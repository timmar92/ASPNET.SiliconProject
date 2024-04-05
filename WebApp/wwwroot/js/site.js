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



document.addEventListener('DOMContentLoaded', function () {
    selectAndSearch();
});

function selectAndSearch() {
    try {
        let form = document.querySelector('.searchbar');
        let select = document.querySelector('#select');
        let searchQuery = document.querySelector('#search-query');

        select.addEventListener('change', function (e) {
            e.preventDefault();
            updateCoursesByFilter()
        });

        searchQuery.addEventListener('keyup', function (e) {
            e.preventDefault();
            updateCoursesByFilter()
        });

        form.addEventListener('keydown', function (e) {
            if (e.key === 'Enter') {
                e.preventDefault();
            }
        });
    }
    catch (error) {
        console.log(error);
    }
};


function updateCoursesByFilter() {
    const category = document.querySelector('#select').value;
    const searchValue = document.querySelector('#search-query').value;

    const url = `/courses/index?category=${encodeURIComponent(category)}&searchQuery=${encodeURIComponent(searchValue)}`;

    fetch(url)
    .then(response => response.text())
        .then(data => {
            const parser = new DOMParser();
            const dom = parser.parseFromString(data, 'text/html');
            document.querySelector('.course-group').innerHTML = dom.querySelector('.course-group').innerHTML;
        });

    console.log(category)
};