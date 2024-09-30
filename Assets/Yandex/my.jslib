mergeInto(LibraryManager.library, {
  	RateGameExt: function () {
    
    	ysdk.feedback.canReview()
        .then(({ value, reason }) => {
            if (value) {
                ysdk.feedback.requestReview()
                    .then(({ feedbackSent }) => {
                        console.log(feedbackSent);
                    })
            } else {
                console.log(reason)
            }
        })
  	},


	SaveExtern: function(date) {
		ysdk.getPlayer({scopes:false}).then(_player=>{
			player = _player;
			var dateString = UTF8ToString(date);
			var myobj = JSON.parse(dateString);
			player.setData(myobj);
		}).catch(err =>{
				 console.log(err);
		});
  	},

  	LoadExtern: function(){
			ysdk.getPlayer({scopes:false}).then(_player=>{
				player = _player;
				player.getData().then(_date =>{
				const myJSON = JSON.stringify(_date);
				myGameInstance.SendMessage('YandexManager', 'SetPlayerData', myJSON);
				});
			}).catch(err =>{
				 console.log(err);
			});
 	},

 	SaveToLeaderBoardExt: function(value){
    	ysdk.getLeaderboards()
      	.then(lb => {
        lb.setLeaderboardScore('Scores', value);
      });
  	},

	GetLang: function () {
    var lang = ysdk.environment.i18n.lang;
    var bufferSize = lengthBytesUTF8(lang) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(lang, buffer, bufferSize);
    return buffer;
    },
    ShowAdv : function(){
        ysdk.adv.showFullscreenAdv({
          callbacks: {
          onClose: function(wasShown) {
            myGameInstance.SendMessage('YandexManager', 'CloseFullScreenAdv');
        	},
          onError: function(error) {
        	}
        }
        })
    },
	
    ShowRew : function(value){
        ysdk.adv.showRewardedVideo({
    callbacks: {
        onOpen: () => {
          console.log('Video ad open.');
        },
        onRewarded: () => {
          console.log('Rewarded!');
          myGameInstance.SendMessage('YandexManager', 'AddCoins', value);
        },
        onClose: () => {
          console.log('Video ad closed.');
        }, 
        onError: (e) => {
          console.log('Error while open video ad:', e);
        }
    }
})
    },
	ShowRewBomb : function(){
        ysdk.adv.showRewardedVideo({
    callbacks: {
        onOpen: () => {
          console.log('Video ad open.');
        },
        onRewarded: () => {
          console.log('Rewarded!');
          myGameInstance.SendMessage('YandexManager', 'AddBomb');
        },
        onClose: () => {
          console.log('Video ad closed.');
        }, 
        onError: (e) => {
          console.log('Error while open video ad:', e);
        }
    }
})
    },
	ShowRewX : function(){
        ysdk.adv.showRewardedVideo({
    callbacks: {
        onOpen: () => {
          console.log('Video ad open.');
        },
        onRewarded: () => {
          console.log('Rewarded!');
          myGameInstance.SendMessage('YandexManager', 'AddCoins', value);
        },
        onClose: () => {
          console.log('Video ad closed.');
        }, 
        onError: (e) => {
          console.log('Error while open video ad:', e);
        }
    }
})
    },
	ShowRewPlusRunTimeCoins : function(value){
        ysdk.adv.showRewardedVideo({
    callbacks: {
        onOpen: () => {
          console.log('Video ad open.');
        },
        onRewarded: () => {
          console.log('Rewarded!');
          myGameInstance.SendMessage('YandexManager', 'AddRunTimeCoins',value);
        },
        onClose: () => {
          console.log('Video ad closed.');
        }, 
        onError: (e) => {
          console.log('Error while open video ad:', e);
        }
    }
})
    },
	GameReadyReady: function(){
    YaGames.init().then((ysdk) => {
        ysdk.features.LoadingAPI.ready()})
    },
	GameReadyStart : function(){
	YaGames.init().then((ysdk) => {
        ysdk.features.GameplayAPI.start()})
    },
	GameReadyStop : function(){
	YaGames.init().then((ysdk) => {
        ysdk.features.GameplayAPI.stop()})
    },
	

  });