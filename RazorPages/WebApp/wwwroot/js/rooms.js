function validateCapacity() {
    var capacity = document.getElementById("capacity").value;
    if (capacity <= 0) {
        document.getElementById("capacityError").style.display = "block";
        return false; 
    }
    return true; 
}

// rooms.js

function validateForm() {
    var roomName = document.getElementById("roomName").value;
    var capacity = document.getElementById("capacity").value;

    // Check if roomName is empty
    if (roomName.trim() == "") {
        alert("Room Name must be filled out");
        return false;
    }

    // Check if capacity is greater than 0
    if (parseInt(capacity) <= 0) {
        document.getElementById("capacityError").style.display = "block";
        return false;
    }

    return true;
}

// Optional: Clear capacity error message on input change
document.getElementById("capacity").addEventListener("input", function() {
    var capacity = document.getElementById("capacity").value;
    if (parseInt(capacity) > 0) {
        document.getElementById("capacityError").style.display = "none";
    }
});
