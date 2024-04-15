

document.addEventListener('DOMContentLoaded', function () {
    let switchButton = document.querySelector('#switch-mode');

    let switchFunction = function (event) {

        let theme = this.checked ? 'dark' : 'light';

        fetch(`/sitesettings/changetheme?mode=${theme}`)
            .then(response => {
                if (response.ok) {
                    window.location.reload();
                }
                else {
                    console.log('Error');
                }
            });
    };

    if (switchButton) {
        switchButton.addEventListener('change', switchFunction);
    }

    let mobileMenuButton = document.querySelector('#btn-mobilemenu');
    let mobileMenu = document.querySelector('#mobile-menu');

    mobileMenuButton.addEventListener('click', function () {
        if (mobileMenu.classList.contains('open')) {
            mobileMenu.classList.remove('open');
        } else {
            mobileMenu.classList.add('open');

            setTimeout(function () {
                let mobileSwitchButton = document.querySelector('#mobile-switch-mode');
                if (mobileSwitchButton) {
                    mobileSwitchButton.removeEventListener('change', switchFunction);
                    mobileSwitchButton.addEventListener('change', switchFunction);
                }
            }, 100);
        }
    });



    selectAndSearch();

    handleUpload();

    hideTempDataMessage();

    let inactiveElements = document.querySelectorAll('.inactive');
    inactiveElements.forEach(function (inactiveElement) {
        inactiveElement.addEventListener('mouseover', function () {
            let activeElement = document.querySelector('.active');
            if (activeElement) {
                activeElement.classList.add('shrink');
            }
        });
        inactiveElement.addEventListener('mouseout', function () {
            let activeElement = document.querySelector('.active');
            if (activeElement) {
                activeElement.classList.remove('shrink');
            }
        });
    });


});

function handleUpload() {
    try {
        let fileUploader = document.querySelector('#upload-file');

        if (fileUploader != undefined) {
            fileUploader.addEventListener('change', function () {
                if (this.files.length > 0) {
                    this.form.submit();
                }
            });
        }
    }
    catch (error) {
        console.log(error);
    }
}



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

            const pagination = dom.querySelector('.pagination') ? dom.querySelector('.pagination').innerHTML : '';
            document.querySelector('.pagination').innerHTML = pagination;
        });

    console.log(category)
};


function hideTempDataMessage() {
    let tempDataMessages = document.querySelectorAll('.tempdata-message');
    tempDataMessages.forEach(tempDataMessage => {
        let parentDiv = tempDataMessage.parentElement;
        if (parentDiv && (parentDiv.classList.contains('error') || parentDiv.classList.contains('success'))) {
            setTimeout(function () {
                parentDiv.style.opacity = '0';
            }, 3000);
        }
    });
}


