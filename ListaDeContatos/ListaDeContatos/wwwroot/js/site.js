// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//### funções
function validarTelefone(idTag) {
    var telefoneMask = $("#" + idTag).val().replace(/[^0-9]/g, "");

    if (telefoneMask.length > 11)
        telefoneMask = telefoneMask.substring(0, 11);

    if (telefoneMask.length == 11)
        return $("#" + idTag).val("(" + telefoneMask.substring(0, 2) + ") " + telefoneMask.substring(2, 7) + "-" + telefoneMask.substring(7, 11));
    else if (telefoneMask.length < 11 && telefoneMask.length > 6)
        return $("#" + idTag).val("(" + telefoneMask.substring(0, 2) + ") " + telefoneMask.substring(2, 6) + "-" + telefoneMask.substring(6, 10));
    else if (telefoneMask.length > 2 && telefoneMask.length < 7) {
        return $("#" + idTag).val("(" + telefoneMask.substring(0, 2) + ") " + telefoneMask.substring(2, 6));
    }
    else if (telefoneMask.length > 0 && telefoneMask.length < 3) {
        return $("#" + idTag).val("(" + telefoneMask);
    }
    else if (telefoneMask.length == 0) {
        return $("#" + idTag).val("(__) ____-____");
    }
}