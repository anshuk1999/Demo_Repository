$(document).ready(function () {
    // Numbers only
    $(".numbers-only").on("input", function () {
        this.value = this.value.replace(/[^0-9]/g, '');
    });

    // Letters + spaces only
    $(".letters-only").on("input", function () {
        this.value = this.value.replace(/[^a-zA-Z\s]/g, '');
    });
    // Mobile number (digits only, max 10)
    $(".mobile-only").on("input", function () {
        this.value = this.value.replace(/[^0-9]/g, ''); // remove non-digits
        if (this.value.length > 10) {
            this.value = this.value.slice(0, 10); // cut off after 10
        }
    });
});

console.log("site.js loaded!");
