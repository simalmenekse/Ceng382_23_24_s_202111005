document.addEventListener("DOMContentLoaded", function() {
    const toggleButton = document.getElementById("toggleElementsButton");
    const toggleableElements = document.getElementById("toggleableElements");

    toggleButton.addEventListener("click", function() {
        if (toggleableElements.style.display === "none") {
            toggleableElements.style.display = "flex"; //FlexGrid
        } else {
            toggleableElements.style.display = "none";
        }
    });
});


function openCalculator() {
    var calculator = document.getElementById('calculator');
    if (calculator.style.display === 'none' || calculator.style.display === '') {
      calculator.style.display = 'block';
    } else {
      calculator.style.display = 'none';
    }
  }
  

  function calculate() {
    var num1 = parseFloat(document.getElementById('num1').value);
    var num2 = parseFloat(document.getElementById('num2').value);
    var resultElement = document.getElementById('result');
    var result = num1 + num2;
    resultElement.innerText = 'Result: ' + result;
  }