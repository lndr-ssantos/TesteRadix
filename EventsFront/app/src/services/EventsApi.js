import axios from 'axios'

export default class EventsApi {
    async getEvents() {
        var url = 'http://localhost:52831/api/events';
        var response = await axios.get(url);

        return response.data;
    }
}