import React, {Component, PropTypes} from 'react';
import {Panel} from 'react-bootstrap';
import {AutoAffix} from 'react-overlays';

const RepositoryDetails = (props) => {
  if (!props.full_name) {
    return <span></span>;
  }
  const header = (
    <span><strong>Full name:</strong> {props.full_name}</span>
  );
  return (
    <AutoAffix viewportOffsetTop={55}>
      <Panel header={header} bsStyle="primary">
        <p><strong>Description:</strong> {props.description}</p>
        <p><strong>Created at:</strong> {props.created_at}</p>
        <p><strong>Stars:</strong> {props.stargazers_count}</p>
        <p><strong>Watchers:</strong> {props.watchers_count}</p>
      </Panel>
    </AutoAffix>
  );
};

RepositoryDetails.propTypes = {
  id: PropTypes.number,
  full_name: PropTypes.string,
  description: PropTypes.string,
  created_at: PropTypes.string,
  stargazers_count: PropTypes.number,
  watchers_count: PropTypes.number,
};

export default RepositoryDetails;
