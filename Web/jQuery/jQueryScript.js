$(document).ready(function () {
    var counter = 1;

    $desktop = $('.desktop1');

    $other = $desktop.siblings('.active');

    $other.each(function() {
        var $this = $(this);
        $this.removeClass('active').animate({
            left: $this.width()
        }, 500);
    });

    $desktop.addClass('active').show().css({
        left: -($desktop.width())
    }).animate({
        left: 0
    }, 500);

    counter++;

    $('.container').click(function() {
        var $desktop;
        if(counter===1) {
            $desktop = $('.desktop1');
        }
        else if(counter===2){
            $desktop = $('.desktop2');
        }
        else if(counter===3){
            $desktop = $('.desktop3');
        }
        else{
            $desktop = $('.desktop4');
        }
        $other = $desktop.siblings('.active');

        $other.each(function() {
            var $this = $(this);
            $this.removeClass('active').animate({
                left: $this.width()
            }, 500);
        });

        $desktop.addClass('active').show().css({
            left: -($desktop.width())
        }).animate({
            left: 0
        }, 500);

        counter++;
        if(counter===5){
            counter=1;
        }
    });
});