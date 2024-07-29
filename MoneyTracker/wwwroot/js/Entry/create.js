document.addEventListener("DOMContentLoaded", function () {
    var today = new Date();
    var day = ("0" + today.getDate()).slice(-2);
    var month = ("0" + (today.getMonth() + 1)).slice(-2);
    var year = today.getFullYear();
    var todayString = year + "-" + month + "-" + day;

    document.getElementById("CreateEntryVM_At").value = todayString;
});