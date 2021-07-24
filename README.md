# TrigramWriters

Trigram writer in PowerShell and C#.
<br><br>
A Trigram writer is a very simple generative algorithm for creating computer generated text. It takes a sample of text and breaks it into each grouping of three words that exist in the text. For example the text "The lazy dog is fat" is broken into 3 trigrams ("The","Lazy","Dog"),("lazy","dog","is") and ("dog","is","fat"). The process works better with longer pieces of text, for example a whole book from project gutenberg has a decent enough sample size to produce a large enough set of trigrams. The Trigram writer is then seeded in some way to start the generated text, perhaps randomly selecting one of the loaded trigrams. From there, the generative algorithm looks at the final two words of the current trigram and finds all of the loaded trigrams that begin with those two words. It then randomly selects one of these trigrams and adds it to the generated text repeating this process for as long as instructed. The output can be entertaining because it is a combination of jiberrish and some semblance of sense based on the semantic rules it has picked up by breaking sentences into chunks of three words.
