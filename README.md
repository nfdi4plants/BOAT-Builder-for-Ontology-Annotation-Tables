# Tutorial

[Builder for Ontology Annotation Tables](https://nfdi4plants.github.io/BOAT-Builder-for-Ontology-Annotation-Tables/) is a web-based tool to direct assist in creating metadata annotations out of your free text protocols. These can be connected with ontologies and you get an MS Excel and ARC compatible output containing your annotations describing experimental processes.

<img src= "src\img\overview.png" width="800">

## Upload your document file

First of all, start by uploading your free text document as a .docx, .pdf, .md or .txt file using the 'Browse' button. The explorer window will open to let you select the file.

<img src= "src\img\upload.svg" width="600">

After a quick load your protocol should be displayed as a whole and can be scrolled.

## Adding and customize an annotation

To add a step of the experimental process as an annotation, mark the word and right-click to open the BOAT context menu.

<img src= "src\img\contextmenu.svg" width="600">

Inside the upper context menu, the marked term can be placed inside a new annotation as a Key, Term or Value. In the lower part, the selected word can be added to the annotation lastly created as the same types.

Here the Key refers to the header of the annotation and Term with an optional value is the content.

After clicking an action inside the context menu, an annotation note is opened at the edge of the document with the marked word in the respective text field.

<img src= "src\img\newAnno.svg" width="600">

Now, you sucessfully added you first annotation! Hooray 🎉

They can be expanded or downsized agin, leading to a small speech bubble icon.

But it might need some adjustments. First, you propably want to change the keytype to be more descriptive for the header. By clicking on the blue box, a dropdown opens which lets you determine the keytype such as Characterisics, Factor, Component or Parameter. Under "more" you can find more descriptives keytypes which are for more general data about the protocol like date, performer, description, type, uri and version. 

<img src= "src\img\keytypeDropdown.png" width="600">

As a next step, you should consider connecting your added key or terms to ontologies.

Implementing ontologies enhances the findability and interoperable aspect of your data by using predefined ontologies with an unique ID.

To do this, just click inside the text field and the built-in search compoenent will search in the database for fitting ontologies. You might change the word a bit to get the best results. The search suggestions can be expanded to see the definitions and the ID of the ontology. 

Clicking on an ontology suggestion replaces the term in the text field and a checkmark appears on the right to ensure the use of an ontology.

<img src= "src\img\termSearch.png" width="600">

In the same way, you can add more annotations or complete the metadata in the last created annotation. Added words as key, terms or value shighlights them in the document in different colors.

<img src= "src\img\highlight.svg" width="600">

All annotations can be edited or deleted any time with the trash bin icon in the upper right corner.

## Preview 

To have an overview of your already annotation, you can always click on "View annotations" to open a table which has all the annotations with their keys, terms and values.
In there, you can also delete individual annotations.

<img src= "src\img\preview.png" width="400">

## Download the finished product

Added enough annotations? Then just click on the download button and decide if the output metadata is converted into a .xlsx or .json file.

<img src= "src\img\download.png" width="600">

It depends on which file format fits to your workflow or enviroment the best for example MS Excel or the ARCitect by [DataPLANT](https://www.nfdi4plants.org/).

And that's it!

If you want to annotate more metadata in a new protocol, you can delete it using the red delete button and the left side of the document or just uplaod a new one which overwrites the current document. 

# Information for developers

## Requirements

* [dotnet SDK](https://www.microsoft.com/net/download/core) v7.0 or higher
* [node.js](https://nodejs.org) v18+ LTS


## Editor

To write and edit your code, you can use either VS Code + [Ionide](http://ionide.io/), Emacs with [fsharp-mode](https://github.com/fsharp/emacs-fsharp-mode), [Rider](https://www.jetbrains.com/rider/) or Visual Studio.


## Development

Before doing anything, start with installing npm dependencies using `npm install`.

Then to start development mode with hot module reloading, run:
```bash
npm start
```
This will start the development server after compiling the project, once it is finished, navigate to http://localhost:8080 to view the application .

To build the application and make ready for production:
```
npm run build
```
This command builds the application and puts the generated files into the `deploy` directory (can be overwritten in webpack.config.js).

### Tests

The template includes a test project that ready to go which you can either run in the browser in watch mode or run in the console using node.js and mocha. To run the tests in watch mode:
```
npm run test:live
```
This command starts a development server for the test application and makes it available at http://localhost:8085.

To run the tests using the command line and of course in your CI server, you have to use the mocha test runner which doesn't use the browser but instead runs the code using node.js:
```
npm test
```