document.addEventListener('DOMContentLoaded', function () {
    let switchButton = document.querySelector('#switch-mode')

    switchButton.addEventListener('change', function () {
        let theme = this.checked ? 'dark' : 'light'

        fetch(`/sitesettings/changetheme?mode=${theme}`)
            .then(response => {
                if (response.ok) {
                    window.location.reload()
                }
                else {
                    console.log('Error')
                }
            });
    });
});
