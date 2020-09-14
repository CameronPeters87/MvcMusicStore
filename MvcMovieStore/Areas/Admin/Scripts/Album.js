$(function() {

    $('input.album-submit').attr("disabled", "disabled");

    $('input.album-title').keyup(function() {

        if($(this).val() == "") {
        $('input.album-submit').attr("disabled", "disabled");
        } 
        else {
        $('input.album-submit').removeAttr("disabled");
        }

    });
})