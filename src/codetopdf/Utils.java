/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package codetopdf;

import static codetopdf.CodeToPdf.ficherosCSENcontrados;
import com.itextpdf.text.BaseColor;
import com.itextpdf.text.Document;
import com.itextpdf.text.DocumentException;
import com.itextpdf.text.Element;
import com.itextpdf.text.Font;
import com.itextpdf.text.Paragraph;
import com.itextpdf.text.pdf.PdfWriter;
import java.io.BufferedReader;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.FileReader;
import java.io.IOException;
import java.util.logging.Level;
import java.util.logging.Logger;

/**
 *
 * @author 1
 */
public class Utils {
    
    private static int generatedPdfs = 0;
    
    public static void getCSFIles(File directorioPrincipal) {
        File[] directorios = directorioPrincipal.listFiles();
        for (File d : directorios) {
            if (d.isDirectory()) {
                File[] ficherosCs = d.listFiles(new CsNameFiltrer());
                if (ficherosCs.length > 0) {
                    for (File f : ficherosCs) {
                        ficherosCSENcontrados.add(new File(f.getAbsolutePath()));
                    }
                } else {
                    getCSFIles(d);
                }
            }
        }
    }
    
    
    private static void genPDF(File file, File directorioProyecto) {
        FileReader fr = null;
        try {
            String line = "";
            BaseColor colorComentario = new BaseColor(186, 255, 201);
            BaseColor colorLibrerias = new BaseColor(186, 225, 255);
            Font fuenteLibreria = new Font(Font.FontFamily.COURIER, 6, Font.BOLD, colorLibrerias);
            Font fuenteHeader = new Font(Font.FontFamily.COURIER, 6, Font.BOLD, BaseColor.YELLOW);
            Font fuenteLlaves = new Font(Font.FontFamily.COURIER, 6, Font.BOLD, BaseColor.ORANGE);
            Font fuenteBase = new Font(Font.FontFamily.COURIER, 6, Font.NORMAL, BaseColor.WHITE);
            Font fuenteComment = new Font(Font.FontFamily.COURIER, 6, Font.BOLD, colorComentario);
            Font fuenteTitulo = new Font(Font.FontFamily.COURIER, 12, Font.BOLD, colorComentario);

            fr = new FileReader(file);
            BufferedReader br = new BufferedReader(fr);

            Document doc = new Document();
            File ficheroPDF = new File(directorioProyecto, file.getName().replace(".cs", ".pdf"));
            PdfWriter writer = PdfWriter.getInstance(doc, new FileOutputStream(ficheroPDF));
            Background event = new Background();
            writer.setPageEvent(event);
            doc.open();
            Paragraph p = new Paragraph(directorioProyecto.getName() + " --- " + file.getName().substring(0, file.getName().indexOf(".")), fuenteTitulo);
            p.setAlignment(Element.ALIGN_CENTER);
            doc.add(p);
            while ((line = br.readLine()) != null) {
                if (line.contains("private") || line.contains("public") || line.contains("protected")) {
                    doc.add(new Paragraph(line, fuenteHeader));

                } else if (line.trim().contains("using")) {
                    doc.add(new Paragraph(line, fuenteLibreria));
                } else if (line.trim().startsWith("//")) {
                    doc.add(new Paragraph(line, fuenteComment));
                } else if (line.contains("{") || line.contains("}")) {
                    doc.add(new Paragraph(line, fuenteLlaves));
                } else {
                    doc.add(new Paragraph(line, fuenteBase));
                }
            }
            br.close();
            fr.close();
            doc.close();
            generatedPdfs++;
        } catch (FileNotFoundException ex) {
            Logger.getLogger(CodeToPdf.class.getName()).log(Level.SEVERE, null, ex);
        } catch (IOException ex) {
            Logger.getLogger(CodeToPdf.class.getName()).log(Level.SEVERE, null, ex);
        } catch (DocumentException ex) {
            Logger.getLogger(CodeToPdf.class.getName()).log(Level.SEVERE, null, ex);
        } finally {
            try {
                fr.close();
            } catch (IOException ex) {
                Logger.getLogger(CodeToPdf.class.getName()).log(Level.SEVERE, null, ex);
            }
        }

    }
    
    
    static void genAll(String nombreDirectorio) {
        File f = new File(nombreDirectorio);
        f.mkdir();
        for (File file : ficherosCSENcontrados) {
            String nombreProyecto = file.getAbsolutePath().replace("\\" + file.getName(), "");
            nombreProyecto = nombreProyecto.substring(nombreProyecto.lastIndexOf("\\") + 1, nombreProyecto.length());
            File directorioProyecto = new File(f, nombreProyecto);
            if (!directorioProyecto.exists()) {
                directorioProyecto.mkdir();
            }
            genPDF(file, directorioProyecto);
        }
        System.out.println("Successfully generated PDFs ["+generatedPdfs+"]");
    }
}
