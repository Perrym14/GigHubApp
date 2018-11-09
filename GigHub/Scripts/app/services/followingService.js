var FollowingService = function () {

    var createFollowing = function (followeeId, done, fail) {
        $.post("/api/following", { followeeId: followeeId })
            .done(done)
            .fail(fail);
    };

    var deleteFollowing = function (followeeId, done, fail) {
        $.ajax({
                url: "/api/following/" + followeeId,
                method: "DELETE"
        })
            .done(done)
            .fail(fail);
    };

    return {
        createFollowing: createFollowing,
        deleteFollowing: deleteFollowing
    }
}();
