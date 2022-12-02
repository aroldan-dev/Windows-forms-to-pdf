/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package codetopdf;

import java.io.File;
import java.io.FilenameFilter;

/**
 *
 * @author 1
 */
public class CsNameFiltrer implements FilenameFilter{

    @Override
    public boolean accept(File dir, String name) {
        return name.endsWith("cs") && !name.contains("Designer")&& !name.contains("Program");
    }
    
}
