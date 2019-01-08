Get-Content .\missingLemmas.txt | ForEach-Object{

    $response = Invoke-RestMethod -Uri http://www.perseus.tufts.edu/hopper/xmlmorph?lang=greek"&"lookup=$_
   
    $_ >> C:\Users\IWANOS\Desktop\lemmaDefinitions.txt

    $response.analyses.analysis.lemma >> C:\Users\IWANOS\Desktop\lemmaDefinitions.txt
    
}