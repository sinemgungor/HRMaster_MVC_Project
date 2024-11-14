document.addEventListener('DOMContentLoaded', function () {
    // TempData'dan maaþ bilgisini alma
    var tempDataElement = document.getElementById('tempDataContainer');
    var salary = parseFloat(tempDataElement.innerText.trim()); // innerText ile veriyi alma

    var amountInput = document.getElementById('amountInput');
    var submitButton = document.getElementById('submitButton');
    var warningSpan = document.getElementById('amountWarning');

    submitButton.addEventListener('click', function (event) {
        var amount = parseFloat(amountInput.value);

        // Miktarýn geçerli bir sayý olup olmadýðýný kontrol et
        if (isNaN(amount) || amount <= 0) {
            warningSpan.innerText = 'Gecerli bir miktar giriniz.';
            warningSpan.style.display = 'block'; // Uyarýyý göster
            event.preventDefault(); // Formu göndermeyi durdur
            return;
        }

        // Maaþýn üç katýndan fazla olup olmadýðýný kontrol et
        if (amount > 3 * salary) {
            warningSpan.innerText = 'Miktar maasin uc katindan fazla olamaz.Maasiniz: ' + salary;
            warningSpan.style.display = 'block'; // Uyarýyý göster
            event.preventDefault(); // Formu göndermeyi durdur
        } else {
            warningSpan.style.display = 'none'; // Uyarýyý gizle
        }
    });
});