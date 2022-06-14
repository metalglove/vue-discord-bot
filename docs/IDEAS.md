# DiscordBot
1. VUE (data from website)
1.1 See what movies are playing for a certain date
1.2 See the top 10 movies at Vue
1.3 See what is "coming soon"
1.4 Search for movie
1.5 Weekly notification for new premiering movies for that week

--> Requires login or cookie?
1.5 What movies have I seen (movie chart just like the lastfm 4x4 chart)
1.6 Automatically add Movie to agenda (when there are new tickets bought)

2. Movie polling (who wants to see what?)
2.1 Check whether a movie suits everyone (in the poll).
2.2 Create an agenda event by mail (To accept or reject).
 

# Endpoints
1. Find information for a movie by id
GET https://www.vuecinemas.nl/movies.json?movie_id=29571

Noteworthy: this also returns the average rating and rating count!

3. Find movies that are "playing now" for a specific cinema id (Eindhoven) within a date range
GET https://www.vuecinemas.nl/movies.json?type=NOW_PLAYING_PREFERED_CINEMAS&filters%5Bcinema_id%5D%5B%5D=in&filters%5Bcinema_id%5D%5B%5D=23&dateOffset=2022-06-14+21%3A07%3A00&range=7

3. Find available date and time for a movie by id and cinema id
GET https://www.vuecinemas.nl/performances.json?movie_id=29571&cinema_ids%5B%5D=23&filters=&dateOffset=2022-06-14+00%3A00%3A00&range=365

4. Get login cookie?
POST https://www.vuecinemas.nl/user/login.json
form data:
- login-email
- login-password

5. Get top 10 movies
GET https://www.vuecinemas.nl/movies.json?type=TOP_10&filters=&dateOffset=2022-06-14+21%3A15%3A00&range=365

6. Get the dates and time for movies which are premiering
GET https://www.vuecinemas.nl/movies.json?type=PRE_PREMIERE&filters=&dateOffset=2022-06-14+21%3A16%3A00&range=365

7. Get the movies which are coming soon
GET https://www.vuecinemas.nl/movies.json?type=EXPECTED&filters=&dateOffset=2022-06-14+21%3A17%3A00&range=730

8. Search for movies for a specific cinema (Eindhoven), genres, experience (beleving) and kijkwijzer 
https://www.vuecinemas.nl/movies.json?type=ADVANCED_SEARCH&limit=0&range=12&filters%5Bcinema_id%5D%5B%5D=in&filters%5Bcinema_id%5D%5B%5D=23&filters%5Bgenres%5D%5B%5D=regexp&filters%5Bgenres%5D%5B%5D=Actie&filters%5Bhas_atmos%5D=1

9. Retrieve tickets
https://www.vuecinemas.nl/mijn-vue/tickets

10. All available movies at Vue
https://www.vuecinemas.nl/movies.json?type=NOW_PLAYING&filters=&dateOffset=2022-06-15+00%3A00%3A00&range=1

11. Find all movies bij special category (i.e. Vue Anime, Sneak preview, Marathon, etc..)
https://www.vuecinemas.nl/movies.json?type=SPECIALS&filters%5Bspecial_category%5D=Vue+Anime&dateOffset=2022-06-14+21%3A21%3A00&range=365

12. See which seats are occupied for a movie (by performance id)
https://www.vuecinemas.nl/services/api/cart.php?action=occupation&performance_id=11034001



