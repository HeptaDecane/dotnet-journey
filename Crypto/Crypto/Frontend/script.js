let key = CryptoJS.enc.Utf8.parse("b14ca5898a4e4142aace2ea2143a2410")
let iv = CryptoJS.enc.Hex.parse("0".repeat(32))
console.log("key", key)
console.log("iv", iv)


document.getElementById("message").oninput = ev=>{
    let message = ev.target.value;
    let cipher = CryptoJS.AES.encrypt(message, key, { iv: iv });
    let plainHex = CryptoJS.AES.decrypt(cipher, key, { iv: iv });

    document.getElementById("message").innerHTML = message;
    document.getElementById("cipher").innerHTML = cipher;
    document.getElementById("plainHex").innerHTML = plainHex;
    document.getElementById("plainText").innerHTML = CryptoJS.enc.Utf8.stringify(plainHex)

    console.log("cipher", cipher)
    console.log("plainHex", plainHex)
    console.log("\n")
}
