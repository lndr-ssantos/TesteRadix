<script>
import { Bar, mixins  } from 'vue-chartjs'
const { reactiveProp } = mixins

export default {
  name: 'BarChart',
  extends: Bar,
  mixins: [reactiveProp],
  props: {
    label: {
      type: String
    },
    chartData: {
      type: Array
    }
  },
  async mounted() {
    var tags = this.chartData.map(e => e.tag);
    var totals = this.chartData.map(e => e.count)

    var chartData = {
      labels: tags,
      datasets: [
        {
          label: 'Eventos Processados',
          data: totals,
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
