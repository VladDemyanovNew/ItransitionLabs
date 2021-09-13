function createSocialNetworkChart(socialNetworks) {
    let providerDisplayNameArr = socialNetworks.map(item => item.providerDisplayName);
    let userCountArr = socialNetworks.map(item => item.userCount);

    const piechart = document.getElementById('pie').getContext('2d');

    const data = {
        labels: providerDisplayNameArr,
        datasets: [{
            label: 'My First Dataset',
            data: userCountArr,
            backgroundColor: [
                'rgb(255, 99, 132)',
                'rgb(54, 162, 235)',
                'rgb(255, 205, 86)'
            ],
            hoverOffset: 4
        }]
    };

    const config = {
        type: 'pie',
        data: data,
    };

    var mypiechart = new Chart(piechart, config);
}
