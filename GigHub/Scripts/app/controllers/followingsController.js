var FollowingsController = function (followingService) {
    var button;

    var init = function () {
        $(".js-toggle-follow").click(toggleFollowing);
    };

    var toggleFollowing = function (e) {
        button = $(e.target);

        var followeeId = button.attr("data-user-id");

        if (button.hasClass("btn-default"))
            followingService.createFollowing(followeeId, done, fail);
        else
            followingService.deleteFollowing(followeeId, done, fail);

    };

    var done = function () {
        var text = (button.text() == "Follow") ? "Following" : "Follow";
        button.toggleClass("btn-info").toggleClass("btn-default").text(text);
    };

    var fail = function() {
        alert("Something failed!");
    };
    return {
        init: init
    }

}(FollowingService);







