$(function () {
    // Enable default jQuery validation behavior
    $('form').validate({
        onkeyup: function (element) {
            $(element).valid();
        },
        onfocusout: function (element) {
            $(element).valid();
        }
    });
});

// Only allow letters and spaces in Name fields
$(document).on('input', '[name="Name"], [name="AccountHolderName"]', function () {
    this.value = this.value.replace(/[^a-zA-Z\s]/g, '');
});

// IFSC Code validation
$(document).on('blur', '[name="IFSCCode"]', function () {
    const $this = $(this);
    const value = $this.val();
    const errorSpan = $this.next('span');

    // Allow empty (let required validation handle it)
    if (!value) {
        $this.removeClass('is-invalid');
        errorSpan.text('');
        return;
    }

    const ifscPattern = /^[A-Z]{4}0[A-Z0-9]{6}$/;

    if (!ifscPattern.test(value)) {
        $this.addClass('is-invalid');
        errorSpan.text('Enter a valid IFSC code');
    } else {
        $this.removeClass('is-invalid');
        errorSpan.text('');
    }
});

// Mobile Number validation
$(document).on('blur', '[name="MobileNumber"]', function () {
    const $this = $(this);
    const value = $this.val();
    const errorSpan = $this.next('span');

    if (!value) {
        $this.removeClass('is-invalid');
        errorSpan.text('');
        return;
    }

    if (!/^\d{10}$/.test(value)) {
        $this.addClass('is-invalid');
        errorSpan.text('Please enter a valid 10-digit mobile number');
    } else {
        $this.removeClass('is-invalid');
        errorSpan.text('');
    }
});

// Email ID validation
$(document).on('blur', '[name="EmailId"], [name="Email"]', function () {
    const $this = $(this);
    const value = $this.val();
    const errorSpan = $this.closest('div').find('span');
    const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

    if (!value) {
        $this.removeClass('is-invalid');
        errorSpan.text('');
        return;
    }

    if (!emailPattern.test(value)) {
        $this.addClass('is-invalid');
        errorSpan.text('Please enter a valid email address');
    } else {
        $this.removeClass('is-invalid');
        errorSpan.text('');
    }
});

//Password and Confirmpassword check both is match
$(document).on('blur input', '[name="Password"], [name="ConfirmPassword"]', function () {
    const password = $('[name="Password"]').val();
    const confirmPassword = $('[name="ConfirmPassword"]').val();
    const $confirmInput = $('[name="ConfirmPassword"]');
    const $errorSpan = $confirmInput.next('span');

    if (!confirmPassword) {
        $confirmInput.removeClass('is-invalid');
        $errorSpan.text('');
        return;
    }

    if (password !== confirmPassword) {
        $confirmInput.addClass('is-invalid');
        $errorSpan.text('Passwords do not match');
    } else {
        $confirmInput.removeClass('is-invalid');
        $errorSpan.text('');
    }
});

// Password required validation fix
$(document).on('blur input', '[name="Password"]', function () {
    const $passwordInput = $(this);
    const password = $passwordInput.val();
    const $errorSpan = $passwordInput.next('span');

    if (!password) {
        $passwordInput.addClass('is-invalid');
        $errorSpan.text('Password is required');
    } else {
        $passwordInput.removeClass('is-invalid');
        $errorSpan.text('');
    }
});

// Fix paste Issue in pincode field
$(document).on('input', '[name="CurrentPincode"], [name="PermanentPincode"], [name="AccountNumber"]', function () {
    this.value = this.value.replace(/[^0-9]/g, '');
});

// Remove validation error
$(function () {
    $('form').find('input, select, textarea, input[type="file"]').on('input blur change', function () {
        $(this).valid();
    });
});

//  This code is use for get Current address Pincode( city, district, state)
$('#CurrentPincode').on('blur', function () {
    var pincode = $(this).val().trim();

    if (pincode.length === 6 && /^\d{6}$/.test(pincode)) {
        const apiUrl = `https://api.postalpincode.in/pincode/${pincode}`;

        $.ajax({
            url: apiUrl,
            method: 'GET',
            success: function (response) {
                if (response[0].Status === "Success" && response[0].PostOffice.length > 0) {
                    var postOffices = response[0].PostOffice;

                    // Set district and state from the first result
                    $('#CurrentDistrict').val(postOffices[0].District);
                    $('#CurrentState').val(postOffices[0].State);

                    // Fill City Datalist
                    var cityList = $('#CityOptions');
                    cityList.empty();
                    cityList.append($('<option>', {
                        value: '',
                        text: 'Select City'
                    }));

                    $.each(postOffices, function (index, po) {
                        cityList.append($('<option>').val(po.Name));
                    });
                } else {
                    alert("No data found for this Pincode.");
                }
            },
            error: function () {
                alert("Error occurred while fetching location data.");
            }
        });
    } else {
        alert("Please enter a valid 6-digit pincode.");
    }
});


// This code is use for get Permanent address Pincode( city, district, state)
$('#PermanentPincode').on('blur', function () {
    var pincode = $(this).val().trim();

    if (pincode.length === 6 && /^\d{6}$/.test(pincode)) {
        const apiUrl = `https://api.postalpincode.in/pincode/${pincode}`;

        $.ajax({
            url: apiUrl,
            method: 'GET',
            success: function (response) {
                if (response[0].Status === "Success" && response[0].PostOffice.length > 0) {
                    var postOffices = response[0].PostOffice;

                    // Set district and state from the first result
                    $('#PermanentDistrict').val(postOffices[0].District);
                    $('#PermanentState').val(postOffices[0].State);

                    // Fill City Datalist
                    var cityList = $('#PermanentCityOptions');
                    cityList.empty();
                    cityList.append($('<option>', {
                        value: '',
                        text: 'Select City'
                    }));

                    $.each(postOffices, function (index, po) {
                        cityList.append($('<option>').val(po.Name));
                    });
                } else {
                    alert("No data found for this Pincode.");
                }
            },
            error: function () {
                alert("Error occurred while fetching location data.");
            }
        });
    } else {
        alert("Please enter a valid 6-digit pincode.");
    }
});


