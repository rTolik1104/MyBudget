﻿var modal = document.getElementById("myModal");

// Get the button that opens the modal
var btn = document.getElementById("myBtn");

// Get the <span> element that closes the modal
var span = document.getElementsByClassName("close1")[0];

// When the user clicks on the button, open the modal
showInPopup = (url, title)=> {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            $('#myModal .modal-body').html(res);
            $('#myModal .modal-title').html(title);
            modal.style.display = "block";
        }
    })
}

// When the user clicks on <span> (x), close the modal
span.onclick = function () {
    modal.style.display = "none";
}

// When the user clicks anywhere outside of the modal, close it
window.onclick = function (event) {
    if (event.target == modal) {
        modal.style.display = "none";
    }
}
