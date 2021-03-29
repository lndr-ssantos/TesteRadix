<template>
  <div>
    <div class="row mt-5">
      <div class="col">
        <bar-chart v-if="eventsProcessed != null" :label="'Eventos Processados'" :chartData="eventsProcessed"></bar-chart>
      </div>
    </div>
  </div>
</template>

<script>
import EventsApi from "@/services/EventsApi"
import BarChart from './BarChart.vue'

export default {
  components: { BarChart },
  name: 'Chart',
  data() {
    return {
      eventsProcessed: null,
    }
  }, 
  methods: {
    async getEventsProcessedByTag() {
      var eventsApi = new EventsApi();

      var events = await eventsApi.getEventsProcessed();
      this.eventsProcessed = events.eventsProcessed;

      console.log(this.eventsProcessed)
    }
  },
  async mounted() {
    await this.getEventsProcessedByTag();
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
