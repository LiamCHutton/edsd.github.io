// wwwroot/js/systemSearch.js
$(document).ready(function () {
    const $input = $('#systemSearch');
    const $suggestions = $('#systemSuggestions');

    $input.on('input', function () {
        const query = $(this).val();
        if (query.length < 3) {
            $suggestions.empty().hide();
            return;
        }

        $.get(`https://www.edsm.net/api-v1/systems?systemName=${encodeURIComponent(query)}&showId=1`, function (data) {
            $suggestions.empty();

            if (Array.isArray(data)) {
                data.slice(0, 10).forEach(system => {
                    $suggestions.append(
                        `<a href="#" class="list-group-item list-group-item-action">${system.name}</a>`
                    );
                });
                $suggestions.show();
            }
        });
    });

    $suggestions.on('click', 'a', function (e) {
        e.preventDefault();
        const selected = $(this).text();
        $input.val(selected);
        $suggestions.empty().hide();
    });

    $(document).on('click', function (e) {
        if (!$(e.target).closest('#systemSearch').length && !$(e.target).closest('#systemSuggestions').length) {
            $suggestions.empty().hide();
        }
    });
});