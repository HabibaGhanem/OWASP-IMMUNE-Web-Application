
function encrypt() {

    //getting a value from a TextBox in the password field.   
    var txtpassword = document.getElementById("password").value;

    //Then encrypting the key and Initialization vector (IV) assigning 
    //and it should be of 16 charaters.
    var key = CryptoJS.enc.Utf8.parse("8080808080808080");
    var iv = CryptoJS.enc.Utf8.parse("8080808080808080");

    //encrypting the value for Password and storing the value in password field.
    var encryptedpassword = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(txtpassword), key,
        {
            keySize: 128 / 8,
            iv: iv,
            mode: CryptoJS.mode.CBC,
            padding: CryptoJS.pad.Pkcs7
        });

    document.getElementById("password").value = encryptedpassword;
}

//var txtpassword = $("#password").val();
//var key = CryptoJS.enc.Hex.parse('000102030405060708090a0b0c0d0e0f');
//var iv = CryptoJS.enc.Hex.parse('101112131415161718191a1b1c1d1e1f');
//var encrypted = CryptoJS.AES.encrypt("Message", key, { iv: iv });
//$("#password").val(encryptedpassword); 
//alert("encrypted password : " + encryptedpassword);
