// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function priwie() {
    var massFileName = document.getElementById("nameFile").files[0];
    var outFile = document.getElementById("outFile");
    outFile.innerHTML = "";
    var img = document.createElement("img");
    img.setAttribute("class", "img-thumbnail");

    var buttonDelete = document.createElement("input");
    buttonDelete.setAttribute("type", "button");
    buttonDelete.setAttribute("class", "btn btn-danger btn-sm");
    buttonDelete.setAttribute("value", "Удалить");
    buttonDelete.setAttribute("onclick", "delPriwie()");

    readFileFromPreview(img, massFileName);

    outFile.append(img);
    outFile.append(buttonDelete);
}

function readFileFromPreview(img, file) {
    var reader = new FileReader();
    reader.onload = (function (theFile) {
        return function (e) {
            img.setAttribute("src", e.target.result);
        };
    })(file);
    reader.readAsDataURL(file);
}

function delPriwie() {
    document.getElementById("outFile").innerHTML = "";
    document.getElementById("nameFile").value = "";
}