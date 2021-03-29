<script>
import { Bar } from 'vue-chartjs'

export default {
  name: 'BarChart',
  extends: Bar,
  props: {
    label: {
      type: String
    },
    chartData: {
      type: Array
    }
  },
  async mounted() {
    console.log(this.chartData)
    var tags = this.chartData.map(e => e.tag);
    var counters = this.chartData.map(e => e.count)

    console.log(tags, counters);

    var chartData = {
      labels: tags,
      datasets: [
      {
        label: 'Eventos Processados',
        data: counters,
        backgroundColor: '#f87979'
        }
      ]
    }

    var chartOptions = {
      responsive: true,
      maintainAspectRatio: false
    }

    this.renderChart(chartData, chartOptions) ;
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>
