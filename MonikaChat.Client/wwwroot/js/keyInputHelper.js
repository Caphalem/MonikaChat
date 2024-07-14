// Used to prevent Tab from unfocusing an element
window.preventKeyDefault = (elementId) => {
    document.getElementById(elementId).addEventListener('keydown', (event) => {
        if (event.key === "Tab") {
            event.preventDefault();
        }
    });
};