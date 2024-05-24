// rooms.js

function validateForm() {
    var roomName = document.getElementById("roomName").value;
    var reservationDate = document.getElementById("reservationDate").value;

    if (roomName.trim() == "") {
        alert("Please select a room.");
        return false;
    }

    if (reservationDate.trim() == "") {
        alert("Please select a reservation date and time.");
        return false;
    }

    var selectedDate = new Date(reservationDate);
    var hours = selectedDate.getHours();
    
    if (hours < 9 || hours >= 18) {
        document.getElementById("timeError").style.display = "block";
        return false;
    } else {
        document.getElementById("timeError").style.display = "none";
    }

    return true;
}
