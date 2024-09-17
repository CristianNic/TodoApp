#!/bin/bash

containers=$(docker ps -a -q)

if [ -n "$containers" ]; then
  docker stop $containers
fi

if [ -n "$containers" ]; then
  docker rm $containers
fi

images=$(docker images -q)

if [ -n "$images" ]; then
  docker rmi $images
fi