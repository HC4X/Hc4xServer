window.addEventListener('DOMContentLoaded', (event) => {
  const inputs = document.querySelectorAll('input, textarea, select');
  if (inputs.length > 0) {
    inputs[0].focus();
  }
});