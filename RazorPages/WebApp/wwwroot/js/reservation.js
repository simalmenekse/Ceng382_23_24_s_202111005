// rooms.js

function validateForm() {
    var roomName = document.getElementById("roomName").value;
    var reservationDate = document.getElementById("reservationDate").value;

    // Check if roomName is selected
    if (roomName.trim() == "") {
        alert("Please select a room.");
        return false;
    }

    // Check if reservationDate is selected
    if (reservationDate.trim() == "") {
        alert("Please select a reservation date and time.");
        return false;
    }

    // You can add more validation logic here as needed

    return true;
}
