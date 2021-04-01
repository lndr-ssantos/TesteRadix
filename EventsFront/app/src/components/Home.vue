<template>
  <div>
    <div v-if="eventsByRegionList.length > 0" class="container">
      <ul class="list-group">
        <li class="list-group-item" v-for="region in eventsByRegionList" :key="region.region">
          {{ region.region }} - {{ region.total }}
          <ul class="list-group">
            <li class="list-group-item" v-for="sensor in region.sensors" :key="sensor[0]" >
              {{ sensor.name }} - {{ sensor.total }}
            </li>
          </ul>
        </li>
      </ul>
    </div>
    <div v-else>
      <p>Não há eventos na base de dados</p>
    </div>
  </div>
</template>

<script>
import EventsApi from "@/services/EventsApi"

export default {
  name: 'Home',
  data() {
    return {
      eventsByRegionList: [],
    }
  }, 
  methods: {
    async getEvents() {
      var eventsApi = new EventsApi();
      var eventsByRegion = await eventsApi.getEventsByRegion();
      this.eventsByRegionList = eventsByRegion.regionsSummaries;
    }
  },
  async mounted() {
    await this.getEvents();
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
