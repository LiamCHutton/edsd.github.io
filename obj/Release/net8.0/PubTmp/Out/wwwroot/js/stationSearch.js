// wwwroot/js/stationSearch.js
$(document).ready(function () {
    const $systemInput = $('#systemSearch');
    const $stationInput = $('#stationName');
    const $stationSuggestions = $('#stationSuggestions');
    const $radiusInput = $('#searchRadius');

    $stationInput.on('input', function () {
        const query = $(this).val();
        const systemName = $systemInput.val();

        if (query.length < 2) {
            $stationSuggestions.empty().hide();
            return;
        }

        $.get(`/Edsm/GetStationSuggestions?systemName=${encodeURIComponent(systemName)}&query=${encodeURIComponent(query)}`, function (data) {
            $stationSuggestions.empty();

            if (Array.isArray(data) && data.length > 0) {
                data.forEach(station => {
                    $stationSuggestions.append(
                        `<a href="#" class="list-group-item list-group-item-action">${station}</a>`
                    );
                });
                $stationSuggestions.show();
            } else {
                $stationSuggestions.hide();
            }
        });
    });

    $stationSuggestions.on('click', 'a', function (e) {
        e.preventDefault();
        const selected = $(this).text();

        // If the suggestion includes a system name in parentheses, extract it
        if (selected.includes('(') && selected.includes(')')) {
            const stationName = selected.substring(0, selected.lastIndexOf('(')).trim();
            const systemName = selected.substring(
                selected.lastIndexOf('(') + 1,
                selected.lastIndexOf(')')
            );

            $stationInput.val(stationName);
            $systemInput.val(systemName);
        } else {
            $stationInput.val(selected);
        }

        $stationSuggestions.empty().hide();
    });

    $(document).on('click', function (e) {
        if (!$(e.target).closest('#stationName').length && !$(e.target).closest('#stationSuggestions').length) {
            $stationSuggestions.empty().hide();
        }
    });

    // Toggle radius input visibility based on whether system name is provided
    $systemInput.on('input', function () {
        toggleRadiusVisibility();
    });

    function toggleRadiusVisibility() {
        const systemName = $systemInput.val();
        const stationName = $stationInput.val();

        if (systemName.length === 0 && stationName.length > 0) {
            $('#radiusContainer').show();
        } else {
            $('#radiusContainer').hide();
        }
    }

    // Initial check
    toggleRadiusVisibility();
});