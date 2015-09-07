# Multithreading
For Schwab

Answer the validation related part

I have created an entity and a model to represent the Quote. Entity class Quote has a method
that returns true or false if the Quote is valid or invalid.
Other class is QuoteWithDataValidationAnnotations, that can be used if the validation is done at the UI.


Answer to the bonus question
My background in multithreadin is limited. I used BackgroundWorker while working on WPF applications.

For this application, I started with Task Parallel Library as this is the latest multithreading API
and provides much better performance over using classical threding approach.
TPL scales the concurrency dynamically to use all the cores efficiently.

Threads come with context switching cost, on the other hand tasks in TPL dynamically determines
if context switching is required or not.

TPL also uses algorithms like Hill climbing to adjust the number of threads and maximize the performance.


About the Solution
I have added the comments in the code to explain some of the decisions made.
Here is additional description about the application.

I have created 2 implementations of the cache. One that uses ReaderWriterLockSlim and another that uses concurrent dictionary.
As explained above there are 2 versions of Quote as well, based on how validation will be done.

For testing the code under multiple threads, I have used Parallel.Invoke 
from TPL and have passed multiple tasks that will either call a get or set on QuoteCache.
The output is displayed on Console as well as in the files corresponding to each task.
Each output has time when thread executed and it can be verified from the timing that 
Get always returns the last value set on quote.




