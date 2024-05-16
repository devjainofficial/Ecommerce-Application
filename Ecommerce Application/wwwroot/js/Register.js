$(document).ready(function () {

    $('#NameError').hide();
    $('#UserNameError').hide();
    $('#EmailError').hide();
    $('#PasswordError').hide();
    $('#ConfirmPasswordError').hide();

    var Name_Error = true;
    var UserName_Error = true;
    var Email_Error = true;
    var Password_Error = true;
    var ConfirmPassword_Error = true;

    $('#Name').keyup(function () {
        name_check();
    });
    $('#UserName').keyup(function () {
        username_check();
    });
    $('#Email').keyup(function () {
        email_check();
    });
    $('#Password').keyup(function () {
        password_check();
    });
    $('#ConfirmPassword').keyup(function () {
        confirmpassword_check();
    });



    function name_check() {
        //
        var NameValue = $('#Name').val();

        if (NameValue == '') {
            $('#NameError').show();
            $('#NameError').html("Please Enter correct Name");
            $('#NameError').focus();
            $('#NameError').css("color", "black");
            Name_Error = false;
            return false;
        }
        else {
            $('#NameError').hide();
            return true;
        }
    }


    function username_check() {
        var UserNameValue = $('#UserName').val();

        if (UserNameValue == '') {
            $('#UserNameError').show();
            $('#UserNameError').html("Please Enter correct Username");
            $('#UserNameError').focus();
            $('#UserNameError').css("color", "black");
            UserName_Error = false;
            return false;
        }
        else {
            $('#UserNameError').hide();
            return true;

        }

        if (UserNameValue.length < 3) {
            $('#UserNameError').show();
            $('#UserNameError').html("Please Enter Username Length more than 3");
            $('#UserNameError').focus();
            $('#UserNameError').css("color", "black");
            UserName_Error = false;
            return false;
        }
        else {
            $('#UserNameError').hide();
            return true;

        }
    }

    function email_check() {
        var EmailValue = $('#Email').val();
        var pattern = /^\b[A-Z0-9._%-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b$/i;

        if (EmailValue == '') {
            $('#EmailError').show();
            $('#EmailError').html("Please Enter correct Email");
            $('#EmailError').focus();
            $('#EmailError').css("color", "black");
            Email_Error = false;
            return false;
        }
        else {
            $('#EmailError').hide();
            return true;

        }

        if (!pattern.test(EmailValue)) {
            $('#EmailError').show();
            $('#EmailError').html("Please Enter correct Email address");
            $('#EmailError').focus();
            $('#EmailError').css("color", "black");
            Email_Error = false;
            return false;
        }
        else {
            $('#EmailError').hide();
            return true;

        }
    }

    function password_check() {
        var PasswordValue = $('#Password').val();
        var pattern = /^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*\W)(?!.* ).{8,16}$/;
        if (PasswordValue == '') {
            $('#PasswordError').show();
            $('#PasswordError').html("Please Enter correct Password");
            $('#PasswordError').focus();
            $('#PasswordError').css("color", "black");
            Password_Error = false;
            return false;
        }
        else {
            $('#PasswordError').hide();
            return true;
        }

        if (!pattern.test(PasswordValue)) {
            $('#PasswordError').show();
            $('#PasswordError').html("Valid password: Atleast, 1 digit, 1 lowercase, 1 uppercase, 1 special, 8-16 chars.");
            $('#PasswordError').focus();
            $('#PasswordError').css("color", "black");
            Password_Error = false;
            return false;
        }
        else {
            $('#PasswordError').hide();
            return true;

        }
    }


    function confirmpassword_check() {
        var PasswordValue = $('#Password').val();
        var ConfirmPasswordValue = $('#ConfirmPassword').val();
        if (ConfirmPasswordValue != PasswordValue) {
            $('#ConfirmPasswordError').show();
            $('#ConfirmPasswordError').html("Password are not Matching");
            $('#ConfirmPasswordError').focus();
            $('#ConfirmPasswordError').css("color", "black");
            ConfirmPassword_Error = false;
            return false;
        }
        else {
            $('#ConfirmPasswordError').hide();
            return true;

        }

    }

    $('#SubmitButton').click(function () {
        //
        var Name_Error = true;
        var UserName_Error = true;
        var Email_Error = true;
        var Password_Error = true;
        var ConfirmPassword_Error = true;
        //
        var namecheck = name_check();
        var usernamechack = username_check();
        var emailcheck = email_check();
        var passwordcheck = password_check();
        var confirmpasswordcheck = confirmpassword_check();

        if ((namecheck == true) && (usernamechack == true) && (emailcheck == true) && (passwordcheck == true) && (confirmpasswordcheck == true)) {
            return true;
        } else {
            return false;
        }

    });

    
    $('#RegisterForm').submit(function (e) {
        debugger
        e.preventDefault();
        var NameValue = $('#Name').val();
        var UserNameValue = $('#UserName').val();
        var EmailValue = $('#Email').val();
        var PasswordValue = $('#Password').val();
        var ConfirmPasswordValue = $('#ConfirmPassword').val();
        var Role = "User";
        var obj = {
            Name: NameValue,
            Username: UserNameValue,
            Email: EmailValue,
            Password: PasswordValue,
            ConfirmPassword: ConfirmPasswordValue,
            Role: Role
            };
        AfterSubmit(obj);
       

    });
    debugger
    function AfterSubmit(obj) {
            debugger
        $.ajax({
            url: '@Url.Action("Register", "Account")',
            type: 'post',
            data: obj,
            datatype:'json',
            success: function (data) {
                
                console.log('submission was successful.');
                window.location.href = '/Home/Index';
            },
            error: function (error) {
                
                console.log('an error occurred.');
                console.log(error);
            }
        });
    }

});
