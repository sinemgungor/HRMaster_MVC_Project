document.getElementById('expenseRequestForm').addEventListener('submit', function (event) {
    event.preventDefault(); // Formun varsayılan davranışını engeller

    const formData = new FormData(this);

    fetch(this.action, {
        method: 'POST',
        body: formData
    })
        .then(response => response.json())
        .then(data => {
            // Başarılı yanıt işlemi
            alert('Harcama talebi başarıyla gönderildi.');
            $('#modal2').modal('hide'); // Modal'ı kapat
        })
        .catch(error => {
            // Hata işlemi
            alert('Bir hata oluştu. Lütfen tekrar deneyin.');
        });
});


