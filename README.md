# Wordchain
A word-chain is a type of puzzle where the challenge is to build a chain of words, starting with one particular word and ending with another. 

Successive entries in the chain must all be real words, and each can differ from the previous word by just one letter. For example, you can get from “cat” to “dog” using the following chain.
```
cat -> cot -> cog -> dog
```

The objective of the challenge was to write a program that accepts start and end words and, using words from the supplied dictionary, builds a word chain between them. The tests cases will be a range of words, increasing in length.

Scoring will be based on the speed, the number of words needed to create the chain (fewest is best), validation and error handling


## Running the application

1. Install the .NET 6 SDK
2. Navigate to the WoerdChain folder
3. Run `dotnet run`
4. Enter a start word
5. Enter an end word
6. See the chain
