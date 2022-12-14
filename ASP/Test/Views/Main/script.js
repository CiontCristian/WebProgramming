var titleToDelete;
var currentBook;

$(document).ready(function () {

    $(function(){
        $("#maindiv").on('click', 'tbody > tr', function(){
            $(this).addClass("selected").siblings().removeClass("selected");
            titleToDelete = $(this).children().first().text();
            currentBook = $(this).children().text();
            alert(currentBook);
            //alert(idToDelete);
            $("#titleDelete").val(titleToDelete);
        });
    });

});