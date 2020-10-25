class Trigram
{
    [string]$firstWord
    [string]$secondWord
    [string]$thirdWord

    Trigram([string]$firstWord,[string]$secondWord,[string]$thirdWord)
    {
        $this.firstWord = $firstWord
        $this.secondWord = $secondWord
        $this.thirdWord = $thirdWord
    }
    
}

Function GetNextTrigram
{
Param([Trigram]$currentTrigram,[Trigram[]]$trigrams)

$options = $trigrams | Where-Object {$_.firstWord -eq $currentTrigram.secondWord -and $_.secondWord -eq $currentTrigram.thirdWord}

if($options.Count -eq 0)
{
    $options = $trigrams
}

$choice = (Get-Random -Maximum $options.Count) -1
$chosen = $options[$choice]
return $chosen

}


$input = "C:\Users\ds_wa\source\repos\TrigramWriters\C#\TrigramWriter\TrigramWriter\alice in wonderland.txt"
Write-Host "Enter the file name containing the text that will be read to generate a new text."
Write-Host ("Press enter for default {0}" -f $input)
$chosenFile = Read-Host
if (-not [string]::IsNullOrEmpty($chosenFile))
{
    $input = $chosenFile
}
Write-Host "How many words should the new text be?"
$wordCount = 0
while ($wordCount -lt 1)
{
    try
    {
        $wordCount = [int]::Parse((Read-Host))
    }
    catch
    {
        Write-Host "Enter a valid integer for the word count."
    }
}
Write-Host "Enter the new filename of the new text including the file extension .txt"
$outFile = Read-Host


$words = (Get-Content $input).Split(" ")
[Trigram[]]$trigrams = for($i=0;$i -le $words.Count - 3; $i++)
{
    New-Object Trigram -ArgumentList $words[$i],$words[$i+1],$words[$i+2]
}


$currentTrigram = new-object Trigram -ArgumentList "","",""
for($writtenWords = 0; $writtenWords -le $wordCount; $writtenWords++)
{
    $currentTrigram = GetNextTrigram -currentTrigram $currentTrigram -trigrams $trigrams
    $currentTrigram.firstWord | Out-File -FilePath $outFile -Append -NoNewline
    " " | Out-File -FilePath $outFile -Append -NoNewLine
}













