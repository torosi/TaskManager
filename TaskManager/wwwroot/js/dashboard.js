

function CreateTaskChart(tasks) {

    const labels = [
        'January',
        'February',
        'March',
        'April',
        'May',
        'June',
        "July",
        "August",
        "September",
        "October",
        "December"
    ];
    const data = {
        labels: labels,
        datasets: [{
            data: tasks,
            backgroundColor: [
                'rgba(27,110,194,1)'
            ],
            borderColor: [
                'rgba(27,110,194,1)'
            ],
            borderWidth: 1
        }]
    };
    const config = {
        type: 'bar',
        data: data,
        options: {
            scales: {
                y: {
                    beginAtZero: true
                }
            },
            plugins: {
                legend: {
                    display: false,
                }
            }
        },
    };

    const myChart = new Chart(
        document.getElementById('taskChart'),
        config
    );


}


