    function validateCapacity() {
        var capacity = document.getElementById("capacity").value;
        if (capacity <= 0) {
            document.getElementById("capacityError").style.display = "block";
            return false; 
        }
        return true; 
    }