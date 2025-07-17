# Tutorial

[Builder for Ontology Annotation Tables](https://nfdi4plants.github.io/BOAT-Builder-for-Ontology-Annotation-Tables/) is a web-based tool to direct assist in creating metadata annotations out of your free text protocols. These can be connected with ontologies and you get an MS Excel and ARC compatible output containing your annotations describing experimental processes.

<img src= "src\img\overview.png" width="800">

## Upload your document file

First of all, start by uploading your free text document as a .docx, .pdf, .md or .txt file using the 'Browse' button. The explorer window will open to let you select the file.

<img src= "src\img\upload.jpg" width="600">

After a quick load your protocol should be displayed as a whole and can be scrolled.

## Adding and customize an annotation!

To add a step of the experimental process as an annotation, mark the word and right-click to open the BOAT context menu.

<img src= "src\img\contextmenu.jpg" width="600">

Inside the upper context menu, the marked term can be placed inside a new annotation as a Key, Term or Value. In the lower part, the selected word can be added to the annotation lastly created as the same types.

Here the Key refers to the header of the annotation and Term with an optional value is the content.

After clicking an action inside the context menu, an annotation note is opened at the edge of the document with the marked word in the respective text field.

<img src= "src\img\newAnno.jpg" width="600">

Now, you sucessfully added you first annotation! Hooray 🎉

But it might need some adjustments. First, you propably want to change the keytype to be more descriptive for the header. By clicking on the blue box, a dropdown opens which lets you determine the keytype such as Characterisics, Factor, Component or Parameter. Under "more" you can find more descriptives keytypes which are for more general data about the protocol like date, performer, description, type, uri and version. 

<img src= "src\img\keytypeDropdown.png" width="600">

As a next step, you should consider connecting your added key or terms to ontologies.

Implementing ontologies enhances the findability and interoperable aspect of your data by using predefined ontologies with an unique ID.

To do this, just click inside the text field and the build in search compoenent will search in the database for fitting onotlogies. You might change the word a bit to get the best results. The search suggestions can be expanded to see the definitions and the ID of the ontology. 

Clicking on a ontology suggestion replaces the term in the text field and a checkmark appears on the right to ensure the use of an ontology.

<img src= "src\img\termSearch.png" width="600">

Adding annotations highlights the charcaters in the document you asigned to it

<img src= "src\img\highlight.jpg" width="600">

## Preview 

To have an overview of your already annotation, you can always click on "View annotations" to open a table which has all the annotations with their keys, terms and values.

<img src= "src\img\preview.png" width="600">

## Download the finished product

Added enough annotations? Then just click on the download button and decide if the output metadata is converted into a .xlsx or .json file.

<img src= "src\img\download.png" width="600">

It depends on which file format fits to your workflow or enviroment the best for example MS Excel or the ARCitect by [DataPLANT](https://www.nfdi4plants.org/).

And that's it!

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