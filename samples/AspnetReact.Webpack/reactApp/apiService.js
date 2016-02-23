import axios from 'axios';

const urlBase = 'https://api.github.com/';

export default {
  searchRepositories(search, language) {
    let url = `${urlBase}search/repositories?q=${search}`;
    if (language) {
      url += `+language:${language}`;
    }
    return axios.get(url)
                .then(response => response.data);
  },
};
