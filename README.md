# SampleCSVValidation
Sample project to verify csv formatting

# Goal: 
Read in a file, an integer column count, and a format, and then check the file to identify rows
 that matched the column ocunt for the given schema and the ones that didn't. Generate result files to the user
 
# --
I used a basic MVC .Net project, utilizing a form to handle the submit of the file. If the page was more complicated, I would 
have probably moved more towards an AngularJS framework, but it seemed overkill for this app

# --
I also used CSVHelper, a library I have used before when dealing with delimited files

# --
While I implemented try catches, I did nothing with the exceptions. Had this been an actual application that would be living somewhere, 
I would most likely create Event Viewer log entries, and more direct notifications for more serious exceptions.

# --
I tried to make the file parsing functionality seperate and flexible. In theory, you could parse any type of delimited file using the process with minimal changes.

# --
The lack of storage was fairly unique to most things that I work with. I wanted to create a process that displayed previously uploaded files and 
their results, but I just didn't have time to implement a file db or an updated app.config setting.

# --
Saving/displaying a history of files and results, getting more granular with why a record was invalid, giving the user a choice of including a header or not,
validating against the header row as opposed to the int input, and giving the option of validating and combining files would be interesting next steps on the 
project
