#!/bin/bash
set -e

docker-compose -f docker-compose.yml -f docker-compose.test.yml up --abort-on-container-exit
# docker logs -f --since $(date +%s) bdd_api_tests_service
