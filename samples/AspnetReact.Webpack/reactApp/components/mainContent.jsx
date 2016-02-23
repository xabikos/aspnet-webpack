import React, {Component, PropTypes} from 'react';
import {Grid, Row, Col} from 'react-bootstrap';

import SearchForm from './searchForm.jsx';
import RepositoriesList from './repositoriesList.jsx';
import RepositoryDetails from './repositoryDetails.jsx';

const MainContent = (props) => (
  <Grid>
    <Row>
      <Col md={3}>
        <SearchForm search={props.search}/>
      </Col>
      <Col md={5}>
        <RepositoriesList repositories={props.repositories} selectRepository={props.selectRepository} />
      </Col>
      <Col md={4}>
        <RepositoryDetails {...props.activeRepository}/>
      </Col>
    </Row>
  </Grid>
);

MainContent.propTypes = {
  search: PropTypes.func.isRequired,
  repositories: PropTypes.array.isRequired,
  selectRepository: PropTypes.func.isRequired,
  activeRepository: PropTypes.object,
};

export default MainContent;
