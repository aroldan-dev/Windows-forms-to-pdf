/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Main.java to edit this template
 */
package codetopdf;

import java.io.File;

import java.util.ArrayList;

/**
 *
 * @author 1
 */
public class CodeToPdf {

    static final String DIRECTORIO_DES_INT = "D:\\BACKUP-CLASE\\2DAM\\desarrolloInterfaces";
    static ArrayList<File> ficherosCSENcontrados = new ArrayList<>();

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {

        String nombreDirectorio = "DesarrolloInterfacesPDFS";
        File directorioPrincipal = new File(DIRECTORIO_DES_INT);
        Utils.getCSFIles(directorioPrincipal);
        Utils.genAll(nombreDirectorio);

    }

    

    

    
}

