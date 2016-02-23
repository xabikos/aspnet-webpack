import React, {Component} from 'react';
import _ from 'lodash';

import ApiService from '../apiService.js';
import MainContent from './mainContent.jsx';

class MainController extends Component {
  constructor(props) {
    super(props);

    this.state = {repositories: [], activeRepository: {}};
    this.handleSearch = this.handleSearch.bind(this);
    this.handleRepositorySelected = this.handleRepositorySelected.bind(this);
  }

  handleSearch(search, language) {
    ApiService.searchRepositories(search, language).then(data => {
      this.setState({repositories: data.items});
    });
  }

  handleRepositorySelected(id) {
    const repo = _.find(this.state.repositories, {id});
    if (repo) {
      this.setState({activeRepository: repo});
    }
  }

  render() {
    return (
      <MainContent
        search={this.handleSearch}
        repositories={this.state.repositories}
        selectRepository={this.handleRepositorySelected}
        activeRepository={this.state.activeRepository}
      />
    );
  }
}

export default MainController;
