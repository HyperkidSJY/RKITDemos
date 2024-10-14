$(document).ready(function() {

    //jQuery
    $('#change-text').click(function() {
        $('#jquery-intro p').text('Text changed using jQuery!');
    });

    //Events in jQuery
    $('#hover-box').hover(
        function() {
            $(this).addClass('highlight');
        }, 
        function() {
            $(this).removeClass('highlight');
        }
    );
    $('#programmatic-event').click(function() {
        $('#hover-box').trigger('mouseenter');
    });

    //jQuery Validation
    $('#simple-form').submit(function(event) {
        event.preventDefault();
        const name = $('#name').val();
        if (name === '') {
            $('#error-message').show();
        } else {
            $('#error-message').hide();
            alert('Form submitted successfully!');
        }
    });

    //jQuery Functions
    $('#list-map').click(function() {
        let items = $('#item-list li').map(function() {
            return $(this).text();
        }).get();
        alert('Mapped List: ' + items.join(', '));
    });

    $('#list-filter').click(function() {
        let filteredItems = $('#item-list li').filter(function() {
            return $(this).text().length % 2 === 0;
        }).map(function() {
            return $(this).text();
        }).get();
        alert('Filtered List (Even Length): ' + filteredItems.join(', '));
    });

    //Regular Expression in jQuery
    $('#regex-check').click(function() {
        const input = $('#regex-input').val();
        const regex = /^hello$/i;
        if (regex.test(input)) {
            $('#regex-result').text('Matched!');
        } else {
            $('#regex-result').text('No Match');
        }
    });

    //Callback Functions in jQuery
    $('#callback-button').click(function() {
        $(this).text('Wait...').delay(1000).queue(function(next) {
            $(this).text('Done!');
            next();
        });
    });

    //Deferred & Promise Object
    $('#start-async').click(function() {
        let deferred = $.Deferred();

        setTimeout(function() {
            let success = true; // Simulate async success or failure
            if (success) {
                deferred.resolve('Async operation completed successfully!');
            } else {
                deferred.reject('Async operation failed!');
            }
        }, 2000);

        deferred.promise().done(function(result) {
            $('#async-result').text(result);
        }).fail(function(error) {
            $('#async-result').text(error);
        });
    });

});
