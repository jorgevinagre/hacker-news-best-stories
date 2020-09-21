# Hacker News Best Stories

## What is it?

An API that fecthes the Hacker News best stories and retrieves the top 20, sorted by score in descending order.

## It uses:

1. [Swashbuckle AspNetCore](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)
2. [Polly](https://github.com/App-vNext/Polly)

## Does it build?

## Install and Run locally

```
git clone https://github.com/jorgevinagre/hacker-news-best-stories.git
cd hacker-news-best-stories
dotnet run --project .\BestStoriesApi\BestStoriesApi.csproj --configuration Release
```
Go to <http://localhost:5000/swagger> for api definition.

Go to <http://localhost:5000/api/beststories/top-twenty> to fetch Hacker News top 20 best stories

## Assumptions

<https://hacker-news.firebaseio.com/v0/beststories.json> already retreives stories ordered by score in descending order.

## Approach taken

To be able serve a potentially large number of requests, and not to overwhelm the Hacker News API with many requests, two measures were taken (although the Hacker News API currently does not have a rate limit):

- Cache: serve as many requests as possible from cache without losing actual score order.
- Limit the number of concurrent requests: using Polly, we can add a BulkheadAsync policy and set the max number of parallel requests allowed and a queue size. This way not too many requests are issued to Hacker api.

__The cache size, maximum parallelism and maximum queue size used in this api are arbitrary values.__

## Room for Improvement

- Add a rate limit to this API: top stories do not change every second most of the time. Imposing a rate limit to api calls (by IP for example) should prevent cache access or hitting the backend api often. The client would have to deal with 429 status codes and throttle the requests.

- The process of retrieving fresh stories is straightforward:

    > Get Best Stories ID´s -> Get Top twenty stories items -> add them to cache

The current process could be handled more efficiently if we chain these operations in a pipeline.
There are two options worth considering handling pipelines:

- TPL Dataflow (each step is a block) – built in control for parallelism and concurrency
- Rx.Net - handles data as composable streams, its not asynchronous but supports asynchronous operations.