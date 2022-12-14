
function redirectToSignUp(){
    $(location).attr('href', 'http://localhost:8080/register.html')
}

function showUsers(users){
    $("#user_list").empty();
    //if(users === "No Matches!"){
    //    alert(users);
    //    $("#user_list").append("<li>"+users+"</li>");
    //    return;
    //}
    users.forEach(entity =>{
        let user = entity;
        $("#user_list").append(
            "<li>" + user.name + " " + user.email + " " + "<img src='images/"+user.picture+"' width='20px' height='20px'>"
        + " " + user.hometown + " " + user.age + "</li>"
        )
    });
}

function showInfo(user){
    $("#name").val(user[0].name);
    $("#email").val(user[0].email);
    $("#address").val(user[0].address);
    $("#picture").val(user[0].picture);
    $("#hometown").val(user[0].hometown);
    $("#age").val(user[0].age);
}

function updateInfo(data){
    data=JSON.parse(data);
    document.getElementById("main").innerText=data;
}

$(document).ready(function(){
    $.getJSON("ProfileController",
        {action: "currentUser"},
        showInfo);

    $("#changeProfile").click(function () {
        $.getJSON("ProfileController",
            {action: "change", name_input:$("#name").val(), email_input:$("#email").val(),
                password_input:$("#passwordd").val(), address_input:$("#address").val(),
                picture_input:$("#picture").val(), hometown_input:$("#hometown").val(),
                age_input:$("#age").val()},
            updateInfo);
    });

    $("#get").click(function () {
        $.getJSON("ProfileController",
            {action: "get", filter: $("#filter").val()},
            showUsers);
    });
});