// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function check(form) {
    if (form.userid.value == "admin" && form.pwd.value == "admin") {
        return true;
    }
    else {
        alert("Error Password or Username")
        return false;
    }
}