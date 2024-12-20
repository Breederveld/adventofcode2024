import System.IO
import Control.Monad
import Data.List
import qualified Data.Map as Map

splitBy delimiter = foldr f [[]]
  where f c l@(x:xs) | c == delimiter = []:l | otherwise = (c:x):xs

prefixesOf :: String -> [String] -> [String]
prefixesOf word parts = filter (\x -> isPrefixOf x word) parts

isPossible :: String -> [String] -> Map.Map String Int -> (Map.Map String Int, Int)
isPossible [] _ countMap = (countMap, 1)
isPossible word parts countMap = getOrInsert countMap word (\() -> do
  let searchParts = (prefixesOf word parts)
  foldl (\(newMap, s) part -> do
    let (mp, ss) = isPossible' word part parts newMap
    let newVal = s + ss
    (Map.insert word newVal mp, newVal)
    ) (countMap, 0) searchParts)

isPossible' :: String -> String -> [String] -> Map.Map String Int -> (Map.Map String Int, Int)
isPossible' word part parts countMap = isPossible (drop (length part) word) parts countMap

getOrInsert :: Map.Map String Int -> String -> (() -> (Map.Map String Int, Int)) -> (Map.Map String Int, Int)
getOrInsert countMap [] _ = (countMap, 0)
getOrInsert countMap word next =
  if (Map.member word countMap) then
    (countMap, countMap Map.! word)
  else do
    let (newMap, newVal) = next ()
    let newerMap = Map.insert word newVal newMap
    (newerMap, newVal)

main = do
  handle <- openFile "input.txt" ReadMode
  contents <- hGetContents handle
  let contentLines = lines contents
  let towels = map (\s -> tail s) (splitBy ',' (" " ++ contentLines !! 0))
  let search = tail (tail contentLines)
  let countMap = Map.fromList [("zzzzzzz", 0)]
  print (foldl (\acc (_, s) -> acc + s) 0 (map (\pat -> isPossible pat (filter (\t -> isInfixOf t pat) towels) countMap) search))
  hClose handle
